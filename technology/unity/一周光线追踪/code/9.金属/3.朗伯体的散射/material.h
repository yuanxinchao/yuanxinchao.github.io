#ifndef MATERIAL_H
#define MATERIAL_H

#include "rtweekend.h"
#include "vec3.h"
#include "ray.h"
struct hit_record;

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
#endif