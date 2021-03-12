#ifndef BVH_H
#define BVH_H
#include "rtweekend.h"

#include "hittable.h"
#include "hittable_list.h"
#include "ray.h"

//继承了hittable 说明可碰撞
class bvh_node: public hittable{
	public:
		bvh_node();
		
		//传入一堆碰撞体 和起止时间
		//调用自己的构造 参数 这堆碰撞体 从0开始 到 size大小结束
		bvh_node(const hittable_list& list, double time0, double time1)
			:bvh_node(list.objects,0,list.objects.size(),time0, time1){}

		//size_t 理解为unsigned int
		// i=start  i<end 遍历src_objects 
		//shared_ptr<hittable> 里面的hittable可能是其他bvh_node,sphere或者任何hittable
		bvh_node(
			const std::vector<shared_ptr<hittable>>& src_objects,
			size_t start, size_t end, double time0, double time1);

		virtual bool hit(
			const ray& r, double t_min, double t_max, hit_record& rec) const override;

		virtual bool bounding_box(double time0, double time1, aabb& output_box) const override;

	public:
		shared_ptr<hittable> left;
		shared_ptr<hittable> right;

		//自己的盒子怎么计算 作为参数传进来？
		aabb box;
};

//包围节点的 包围盒计算就返回自己的盒子
bool bvh_node:: bounding_box(double time0, double time1, aabb& output_box) const{
	output_box = box;
	return true;
}

bool bvh_node::hit(const ray&& r, double t_min, double t_max, hit_record& rec) const{
	if(!box.hit(r, t_min, t_max))
		return false;

	bool hit_left = left-> hit(r, t_min, t_max, rec);
	//击中左边 那么，也有可能会击中右边，但是一定要在击中左边之前击中右边，则t_max赋值为rec.t 即表示最大不超过击中左边包围盒时的t
	bool hit_right = right->hit(r, t_min, hit_left?rec.t:t_max, rec);
	return hit_left || hit_right;
}
#endif // ! BVH

