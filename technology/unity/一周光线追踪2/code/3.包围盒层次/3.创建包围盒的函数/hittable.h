#ifndef HITTABLE_H
#define HITTABLE_H

#include "ray.h"
#include "aabb.h"
class material;
struct hit_record {
    point3 p;//��¼��ײ��
    vec3 normal;//��¼��ײ��ķ���

	//ָ���ָ��material��
	//���ָ���¼��������Ĳ���
	shared_ptr<material> mat_ptr;

    double t;//��¼��ײʱ���ߵ�tֵ

	//�������������뷨�ߵļн��ж��ǲ������� �нǴ���90��Ϊ����
    bool front_face;
    inline void set_face_normal(const ray& r, const vec3& outward_normal) {
        front_face = dot(r.direction(), outward_normal) < 0;
        normal = front_face ? outward_normal :-outward_normal;
    }
};
class hittable {
    public:
        virtual bool hit(const ray& r, double t_min, double t_max, hit_record& rec) const = 0;
		//������Χ�еĳ�����
		virtual bool bounding_box(double time0, double time1, aabb& output_box) const = 0;
};
#endif

