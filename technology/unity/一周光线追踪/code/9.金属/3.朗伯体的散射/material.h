#ifndef MATERIAL_H
#define MATERIAL_H

#include "rtweekend.h"
#include "vec3.h"
#include "ray.h"
struct hit_record;

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
#endif