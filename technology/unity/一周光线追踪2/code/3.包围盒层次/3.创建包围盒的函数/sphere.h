#ifndef SPHERE_H
#define SPHERE_H

#include "hittable.h"
#include "vec3.h"

class sphere : public hittable {
    public:
        sphere() {}
		//使用圆心位置 和 半径 初始化
        sphere(point3 cen, double r, shared_ptr<material> m) : center(cen), radius(r), mat_ptr(m) {};

        virtual bool hit(
            const ray& r, double tmin, double tmax, hit_record& rec) const override;

		virtual bool bounding_box(
			double time0, double time1, aabb& output_box) const override;

    public:
        point3 center;
        double radius;
		shared_ptr<material> mat_ptr;
};

//实现hit方法
bool sphere::hit(const ray& r, double t_min, double t_max, hit_record& rec) const {

	//解二元一次方程
    vec3 oc = r.origin() - center;
    auto a = r.direction().length_squared();
    auto half_b = dot(oc, r.direction());
    auto c = oc.length_squared() - radius*radius;
    auto discriminant = half_b*half_b - a*c;

	//有根
    if (discriminant > 0) {
        auto root = sqrt(discriminant);

        auto temp = (-half_b - root) / a;
		//根在 (t_min,t_max) 范围内

        if (temp < t_max && temp > t_min) {
			//记录t值
            rec.t = temp;
			//记录交点位置
            rec.p = r.at(rec.t);
			//记录归一化的法线  
            vec3 outward_normal = (rec.p - center) / radius;
			//记录法线 且必要时翻转法线使光线击中正面
            rec.set_face_normal(r, outward_normal);
			rec.mat_ptr = mat_ptr;//记录材质
            return true;
        }

        temp = (-half_b + root) / a;
        if (temp < t_max && temp > t_min) {
            rec.t = temp;
            rec.p = r.at(rec.t);
            vec3 outward_normal = (rec.p - center) / radius;
            rec.set_face_normal(r, outward_normal);
			rec.mat_ptr = mat_ptr;//记录材质
            return true;
        }
    }

    return false;
}

bool sphere::bounding_box(double time0, double time1, aabb& output_box) const {
    output_box = aabb(
		//包围盒左前下 保证三值都是最小的
        center - vec3(radius, radius, radius),

		//包围盒右后上 保证三值都是最大的
        center + vec3(radius, radius, radius));
    return true;
}
#endif