#ifndef AABB_H
#define AABB_H
#include "rtweekend.h"
#include "vec3.h"
#include "ray.h"


//一个包围盒
class aabb {
	public:
		aabb() {}
		aabb(const point3& a,const point3& b){minimum = a; maximum = b;}

		point3 min() const {return minimum;};
		point3 max() const{return maximum;};

		bool hit(const ray& r, double t_min, double t_max) const;


		point3 minimum;//左下角
		point3 maximum;//右上角
};
inline bool aabb::hit(const ray& r, double t_min, double t_max) const {
    for (int a = 0; a < 3; a++) {
		//先计算invD 不用计算两遍
        auto invD = 1.0f / r.direction()[a];
        auto t0 = (min()[a] - r.origin()[a]) * invD;
        auto t1 = (max()[a] - r.origin()[a]) * invD;

		//如果invD是负数，就交换结果。  这需要组成包围盒的minimum的三个分量要小于等于maximum的三个分量
        if (invD < 0.0f)
            std::swap(t0, t1);

		//取t0和t_min中最大的
        t_min = t0 > t_min ? t0 : t_min;
		//取t1和t_max中最小的
        t_max = t1 < t_max ? t1 : t_max;
        if (t_max <= t_min)
            return false;
    }
    return true;
}
#endif // !AABB_H
