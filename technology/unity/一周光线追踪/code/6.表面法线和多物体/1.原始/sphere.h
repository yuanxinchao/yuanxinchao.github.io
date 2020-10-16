#ifndef SPHERE_H
#define SPHERE_H

#include "hittable.h"
#include "vec3.h"

class sphere : public hittable {
    public:
        sphere() {}
		//ʹ��Բ��λ�� �� �뾶 ��ʼ��
        sphere(point3 cen, double r) : center(cen), radius(r) {};

        virtual bool hit(
            const ray& r, double tmin, double tmax, hit_record& rec) const override;

    public:
        point3 center;
        double radius;
};

//ʵ��hit����
bool sphere::hit(const ray& r, double t_min, double t_max, hit_record& rec) const {

	//���Ԫһ�η���
    vec3 oc = r.origin() - center;
    auto a = r.direction().length_squared();
    auto half_b = dot(oc, r.direction());
    auto c = oc.length_squared() - radius*radius;
    auto discriminant = half_b*half_b - a*c;

	//�и�
    if (discriminant > 0) {
        auto root = sqrt(discriminant);

        auto temp = (-half_b - root) / a;
		//���� (t_min,t_max) ��Χ��

        if (temp < t_max && temp > t_min) {
			//��¼tֵ
            rec.t = temp;
			//��¼����λ��
            rec.p = r.at(rec.t);
			//��¼��һ���ķ���  
            rec.normal = (rec.p - center) / radius;
            return true;
        }

        temp = (-half_b + root) / a;
        if (temp < t_max && temp > t_min) {
            rec.t = temp;
            rec.p = r.at(rec.t);
            rec.normal = (rec.p - center) / radius;
            return true;
        }
    }

    return false;
}


#endif