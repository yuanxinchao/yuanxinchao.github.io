#ifndef MATERIAL_H
#define MATERIAL_H

#include "rtweekend.h"
#include "ray.h"
#include "vec3.h"

struct hit_record;

//反射
//v 入射射线
vec3 reflect(const vec3& v, const vec3& n) {
	//-dot(v,n)*n是向量B ,所以V+2B为 v - 2*dot(v,n)*n
    return v - 2*dot(v,n)*n;
}

//折射
//uv 入射射线？
vec3 refract(const vec3& uv,const vec3& n,double etai_over_etat){
	auto cos_theta = dot(-uv,n);

	//出射射线 垂直于n轴方向分量
	vec3 r_out_perp = etai_over_etat * (uv + cos_theta * n);
	vec3 r_out_parallel = - sqrt(fabs(1.0 - r_out_perp.length_squared() )) * n;

	return r_out_perp + r_out_parallel;
}

class material {
    public:
        virtual bool scatter(
			//r_in 入射射线
			//hit_record 是多次反射过来的吗
			//attenuation 衰减？
			//scattered 散射出去的射线
            const ray& r_in, const hit_record& rec, color& attenuation, ray& scattered
        ) const = 0;
};


class lambertian : public material {
    public:
        lambertian(const color& a) : albedo(a) {}

        virtual bool scatter(
            const ray& r_in, const hit_record& rec, color& attenuation, ray& scattered
        ) const override {
			// 归一化的碰撞点法线向量+指向随机方向的单位向量
			// 最终结果，击中点指向 击中点上方相切球面上一点
            vec3 scatter_direction = rec.normal + random_unit_vector();

            scattered = ray(rec.p, scatter_direction, r_in.time());//出射射线
            attenuation = albedo;
            return true;
        }

    public:
        color albedo;
};


class metal : public material {
    public:
        metal(const color& a, double f) :
			//最大为1 因为求出来的反射射线reflected 大小也仅仅是1
			albedo(a), fuzz(f<1 ?f:1) {}

        virtual bool scatter(
            const ray& r_in, const hit_record& rec, color& attenuation, ray& scattered
        ) const override {
            vec3 reflected = reflect(unit_vector(r_in.direction()), rec.normal);

			//这里很巧妙的使用向量相加
			//反射的向量再加上一个随机方向大小为fuzz的向量 来进行反射向量的角度偏移
            scattered = ray(rec.p, reflected + fuzz*random_in_unit_sphere(), r_in.time());
            attenuation = albedo;
            return (dot(scattered.direction(), rec.normal) > 0);
        }

    public:
        color albedo;

		//用来进行模糊的球的半径
		double fuzz;
};

//添加电介质的材质

class dielectric :public material {
	public:
		dielectric(double index_of_refraction):ir(index_of_refraction){}
	virtual bool scatter(const ray& r_in, const hit_record& rec, color& attenuation, ray& scattered)
		const override{
			attenuation = color(1.0,1.0,1.0);

			//这里我们默认是空气和本电介质发生的接触
			//入射射线为正面  从空气折射进入本电介质材质  1.0/ir
			//入射射线为反面  从电介质内部折射进入空气		ir
			double refraction_ratio = rec.front_face ? (1.0/ir):ir;
			
			//单位入射射线
			vec3 unit_direction = unit_vector(r_in.direction());

	

			//求-v 与法线的夹角的cos
			double cos_theta = fmin(dot(-unit_direction, rec.normal), 1.0);
            double sin_theta = sqrt(1.0 - cos_theta*cos_theta);

			//当从电介质射入空气中时 有可能会发射全发射
			//即η/η' * sin_theta 大于1，这时求不出解 即发生全发射
			bool cannot_refract = refraction_ratio * sin_theta > 1.0;
            vec3 direction;

			//全反射 或者 大于某个强度 这条射线我们就以反射处理
			if(cannot_refract || reflectance(cos_theta, refraction_ratio) > random_double()){
				direction = reflect(unit_direction, rec.normal);
			}else
				direction = refract(unit_direction, rec.normal, refraction_ratio);

			scattered = ray(rec.p, direction, r_in.time());
			return true;
	}


	public:
		double ir;//折射系数


	private:
		//当光照方向和观察方向夹角逐渐增大时高光反射强度增大的现象
		//cosine 为n和v的夹角
		static double reflectance(double cosine, double ref_idx){
			//使用Schlick's 近似来反射
			//ref_idx
			auto r0 = (1 - ref_idx)/(1 + ref_idx);
			r0 = r0 *r0;
			return r0 + (1- r0)* pow((1- cosine),5);
		}

};

#endif