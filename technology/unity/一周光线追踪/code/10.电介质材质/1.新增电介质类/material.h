#ifndef MATERIAL_H
#define MATERIAL_H

#include "rtweekend.h"
#include "ray.h"
#include "vec3.h"

struct hit_record;

//����
//v ��������
vec3 reflect(const vec3& v, const vec3& n) {
	//-dot(v,n)*n������B ,����V+2BΪ v - 2*dot(v,n)*n
    return v - 2*dot(v,n)*n;
}

//����
//uv �������ߣ�
vec3 refract(const vec3& uv,const vec3& n,double etai_over_etat){
	auto cos_theta = dot(-uv,n);
	//�������� ��ֱ��n�᷽�����
	vec3 r_out_perp = etai_over_etat * (uv + cos_theta * n);
	vec3 r_out_parallel = - sqrt(fabs(1.0 - r_out_perp.length_squared() )) * n;

	return r_out_perp + r_out_parallel;
}

class material {
    public:
        virtual bool scatter(
			//r_in ��������
			//hit_record �Ƕ�η����������
			//attenuation ˥����
			//scattered ɢ���ȥ������
            const ray& r_in, const hit_record& rec, color& attenuation, ray& scattered
        ) const = 0;
};


class lambertian : public material {
    public:
        lambertian(const color& a) : albedo(a) {}

        virtual bool scatter(
            const ray& r_in, const hit_record& rec, color& attenuation, ray& scattered
        ) const override {
			// ��һ������ײ�㷨������+ָ���������ĵ�λ����
			// ���ս�������е�ָ�� ���е��Ϸ�����������һ��
            vec3 scatter_direction = rec.normal + random_unit_vector();

            scattered = ray(rec.p, scatter_direction);//��������
            attenuation = albedo;
            return true;
        }

    public:
        color albedo;
};


class metal : public material {
    public:
        metal(const color& a, double f) :
			//���Ϊ1 ��Ϊ������ķ�������reflected ��СҲ������1
			albedo(a), fuzz(f<1 ?f:1) {}

        virtual bool scatter(
            const ray& r_in, const hit_record& rec, color& attenuation, ray& scattered
        ) const override {
            vec3 reflected = reflect(unit_vector(r_in.direction()), rec.normal);

			//����������ʹ���������
			//����������ټ���һ����������СΪfuzz������ �����з��������ĽǶ�ƫ��
            scattered = ray(rec.p, reflected + fuzz*random_in_unit_sphere());
            attenuation = albedo;
            return (dot(scattered.direction(), rec.normal) > 0);
        }

    public:
        color albedo;

		//��������ģ������İ뾶
		double fuzz;
};

//��ӵ���ʵĲ���

class dielectric :public material {
	public:
		dielectric(double index_of_refraction):ir(index_of_refraction){}
	virtual bool scatter(const ray& r_in, const hit_record& rec, color& attenuation, ray& scattered)
		const override{
			attenuation = color(1.0,1.0,1.0);

			//��������Ĭ���ǿ����ͱ�����ʷ����ĽӴ�
			//��������Ϊ����  �ӿ���������뱾����ʲ���  1.0/ir
			//��������Ϊ����  �ӵ�����ڲ�����������		ir
			double refraction_ratio = rec.front_face ? (1.0/ir):ir;
			
			//��λ��������
			vec3 unit_direction = unit_vector(r_in.direction());

			//������������ 
			vec3 refracted = refract(unit_direction, rec.normal, refraction_ratio);
	
			
			scattered = ray(rec.p, refracted);
			return true;
	}


	public:
		double ir;//����ϵ��

};

#endif