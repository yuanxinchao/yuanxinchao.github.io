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

		bool hit(const ray& r,double t_min,double t_max)const{


			for(int a = 0; a<3;a++){
				//a=0 投影到xz平面 与 x0 x1 求得的tx0 tx1
				//a=1 投影到yx平面 与 y0 y1 求得的ty0 ty1
				//a=2 投影到zy平面 与 z0 z1 求得的tz0 tz1
				//注:c++中 分母为0时 分子为正数，得到正无穷  分子为负数，得到负无穷  分子为0 不知道
				auto tt0 = (minimum[a] - r.origin()[a])/r.direction()[a];
				auto tt1 = (maximum[a] - r.origin()[a])/ r.direction()[a];

				//求出来的结果调整下顺序让t0 <= t1
				auto t0 = fmin(tt0,tt1);
				auto t1 = fmax(tt0,tt1);

				//a=0时 是将tx0 tx1与我们自己定义的范围取下交集
				//a=1 是将ty0 ty1 与(tx0 tx1) (t_min t_max)取交集 
				//a=2 是将tz0 tz1 与(ty0 ty1) (tx0 tx1) (t_min t_max)取交集 
				t_min = fmax(t0, t_min);
                t_max = fmin(t1, t_max);


				if(t_max <= t_min)
					return false;
			}
			return true;
		}

		point3 minimum;//左下角
		point3 maximum;//右上角
};
#endif // !AABB_H
