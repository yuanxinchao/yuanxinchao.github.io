#ifndef HITTABLE_H
#define HITTABLE_H

#include "ray.h"
#include "material.h"


struct hit_record {
    point3 p;//记录碰撞点
    vec3 normal;//记录碰撞点的法线

	//指针仅指向material类
	//这个指针记录击中物体的材质
	shared_ptr<material> mat_ptr;

    double t;//记录碰撞时射线的t值
    bool front_face;
    inline void set_face_normal(const ray& r, const vec3& outward_normal) {
        front_face = dot(r.direction(), outward_normal) < 0;
        normal = front_face ? outward_normal :-outward_normal;
    }
};
class hittable {
    public:
        virtual bool hit(const ray& r, double t_min, double t_max, hit_record& rec) const = 0;
};
#endif

