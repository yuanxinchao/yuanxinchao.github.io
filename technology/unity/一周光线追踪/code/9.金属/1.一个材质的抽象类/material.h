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

#endif