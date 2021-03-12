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

		bool hit(const ray& r,double t_min,double t_max)const{


			for(int a = 0; a<3;a++){
				//a=0 ͶӰ��xzƽ�� �� x0 x1 ��õ�tx0 tx1
				//a=1 ͶӰ��yxƽ�� �� y0 y1 ��õ�ty0 ty1
				//a=2 ͶӰ��zyƽ�� �� z0 z1 ��õ�tz0 tz1
				//ע:c++�� ��ĸΪ0ʱ ����Ϊ�������õ�������  ����Ϊ�������õ�������  ����Ϊ0 ��֪��
				auto tt0 = (minimum[a] - r.origin()[a])/r.direction()[a];
				auto tt1 = (maximum[a] - r.origin()[a])/ r.direction()[a];

				//������Ľ��������˳����t0 <= t1
				auto t0 = fmin(tt0,tt1);
				auto t1 = fmax(tt0,tt1);

				//a=0ʱ �ǽ�tx0 tx1�������Լ�����ķ�Χȡ�½���
				//a=1 �ǽ�ty0 ty1 ��(tx0 tx1) (t_min t_max)ȡ���� 
				//a=2 �ǽ�tz0 tz1 ��(ty0 ty1) (tx0 tx1) (t_min t_max)ȡ���� 
				t_min = fmax(t0, t_min);
                t_max = fmin(t1, t_max);


				if(t_max <= t_min)
					return false;
			}
			return true;
		}

		point3 minimum;//���½�
		point3 maximum;//���Ͻ�
};
#endif // !AABB_H
