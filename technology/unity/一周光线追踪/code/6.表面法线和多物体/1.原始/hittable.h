#ifndef HITTABLE_H
#define HITTABLE_H

#include "ray.h"

struct hit_record {
    point3 p;//��¼��ײ��
    vec3 normal;//��¼��ײ��ķ���
    double t;//��¼��ײʱ���ߵ�t
};

class hittable {
    public:
        virtual bool hit(const ray& r, double t_min, double t_max, hit_record& rec) const = 0;
};

#endif