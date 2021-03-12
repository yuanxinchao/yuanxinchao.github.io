#ifndef AABB_H
#define AABB_H
#include "rtweekend.h"
#include "vec3.h"
#include "ray.h"


//һ����Χ��
class aabb {
	public:
		aabb() {}
		aabb(const point3& a,const point3& b){minimum = a; maximum = b;}

		point3 min() const {return minimum;};
		point3 max() const{return maximum;};

		bool hit(const ray& r, double t_min, double t_max) const;


		point3 minimum;//���½�
		point3 maximum;//���Ͻ�
};
inline bool aabb::hit(const ray& r, double t_min, double t_max) const {
    for (int a = 0; a < 3; a++) {
		//�ȼ���invD ���ü�������
        auto invD = 1.0f / r.direction()[a];
        auto t0 = (min()[a] - r.origin()[a]) * invD;
        auto t1 = (max()[a] - r.origin()[a]) * invD;

		//���invD�Ǹ������ͽ��������  ����Ҫ��ɰ�Χ�е�minimum����������ҪС�ڵ���maximum����������
        if (invD < 0.0f)
            std::swap(t0, t1);

		//ȡt0��t_min������
        t_min = t0 > t_min ? t0 : t_min;
		//ȡt1��t_max����С��
        t_max = t1 < t_max ? t1 : t_max;
        if (t_max <= t_min)
            return false;
    }
    return true;
}
#endif // !AABB_H
