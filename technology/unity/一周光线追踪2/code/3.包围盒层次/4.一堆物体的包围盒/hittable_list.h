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

		//�������
		void clear(){objects.clear();}

		//��ӻ��е�����
		void add(shared_ptr<hittable> object){objects.push_back(object);}

		virtual bool hit(
			const ray& r, double tmin, double tmax,hit_record& rec) const override;

		virtual bool bounding_box(
			double time0, double time1, aabb& output_box) const override;

	public:
		//vector �൱��list�� �������пɱ����е�����
		std::vector<shared_ptr<hittable>> objects;

};

//�ж� tֵ��(t_min,t_max)��Χ�� ������û�л�������
bool hittable_list::hit(const ray& r, double t_min, double t_max, hit_record& rec) const{
	//�û��еļ�¼
	hit_record temp_rec;
	//�Ƿ��������
	bool hit_anything = false;
	
	auto closest_so_far = t_max;

	//���� hittable_list�����objects  �������Ƿ����߻���
	for(const auto& object : objects){
		if(object-> hit(r, t_min, closest_so_far,temp_rec) && temp_rec.t < closest_so_far){
			hit_anything = true;
			closest_so_far = temp_rec.t;
			rec = temp_rec;
		}
	}
	return hit_anything;
}

//ʵ��һ������İ�Χ�м���
bool hittable_list::bounding_box(double time0, double time1, aabb& output_box) const{
	if(objects.empty())
		return false;

	aabb temp_box;
	bool first_box = true;

	for(const auto& object : objects){

		//�������һ��objectû�б߽磬��ô����false
		//����б߽� ��ô������������temp_box
		if(!object->bounding_box(time0,time1,temp_box))
			return false;
		//��һ�� �øռ������temp_box, 
		//����� ����һ�μ������output_box �� ��һ�μ������temp_box
		output_box = first_box ? temp_box : surrounding_box(output_box,temp_box);
		first_box = false;
	}
	return true;
}

#endif