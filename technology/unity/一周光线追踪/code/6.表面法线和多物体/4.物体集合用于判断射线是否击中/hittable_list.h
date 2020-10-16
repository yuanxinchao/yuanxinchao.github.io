#ifndef HITTABLE_LIST_H
#define HITTABLE_LIST_H

#include "hittable.h"

#include <memory>
#include <vector>

using std::shared_ptr;
using std::make_shared;

class hittable_list : public hittable
{
	public: 
		hittable_list() {}
		hittable_list(shared_ptr<hittable> object){add(object);}

		//清除所有
		void clear(){objects.clear();}

		//添加击中的物体
		void add(shared_ptr<hittable> object){objects.push_back(object);}

		virtual bool hit(
			const ray& r, double tmin, double tmax,hit_record& rec) const override;

	public:
		std::vector<shared_ptr<hittable>> objects;

};

//判断 t值在(t_min,t_max)范围内 射线有没有击中物体
bool hittable_list::hit(const ray& r, double t_min, double t_max, hit_record& rec) const{
	//拿击中的记录
	hit_record temp_rec;
	//是否击中物体
	bool hit_anything = false;
	
	auto closest_so_far = t_max;

	//遍历 hittable_list储存的objects  看物体是否被射线击中
	for(const auto& object : objects){
		if(object-> hit(r, t_min, closest_so_far,temp_rec)){
			hit_anything = true;
			closest_so_far = temp_rec.t;
			rec = temp_rec;
		}
	
	}
	return hit_anything;
}

#endif