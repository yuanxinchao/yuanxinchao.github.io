#ifndef MATERIAL_H
#define MATERIAL_H

#include "rtweekend.h"
#include "ray.h"
#include "vec3.h"

struct hit_record;
vec3 reflect(const vec3& v, const vec3& n) {
	//-dot(v,n)*n������B ,����V+2BΪ v - 2*dot(v,n)*n
    return v - 2*dot(v,n)*n;
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
        metal(const color& a) : albedo(a) {}

        virtual bool scatter(
            const ray& r_in, const hit_record& rec, color& attenuation, ray& scattered
        ) const override {
            vec3 reflected = reflect(unit_vector(r_in.direction()), rec.normal);
            scattered = ray(rec.p, reflected);
            attenuation = albedo;
            return (dot(scattered.direction(), rec.normal) > 0);
        }

    public:
        color albedo;
};

#endif