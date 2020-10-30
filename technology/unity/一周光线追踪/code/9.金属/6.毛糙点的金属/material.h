#ifndef MATERIAL_H
#define MATERIAL_H

#include "rtweekend.h"
#include "ray.h"
#include "vec3.h"

struct hit_record;
vec3 reflect(const vec3& v, const vec3& n) {
	//-dot(v,n)*n是向量B ,所以V+2B为 v - 2*dot(v,n)*n
    return v - 2*dot(v,n)*n;
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

            scattered = ray(rec.p, scatter_direction);//出射射线
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
            scattered = ray(rec.p, reflected + fuzz*random_in_unit_sphere());
            attenuation = albedo;
            return (dot(scattered.direction(), rec.normal) > 0);
        }

    public:
        color albedo;

		//用来进行模糊的球的半径
		double fuzz;
};

#endif