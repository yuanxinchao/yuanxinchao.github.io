#ifndef BVH_H
#define BVH_H
#include "rtweekend.h"

#include "hittable.h"
#include "hittable_list.h"
#include "ray.h"

//�̳���hittable ˵������ײ
class bvh_node: public hittable{
	public:
		bvh_node();
		
		//����һ����ײ�� ����ֹʱ��
		//�����Լ��Ĺ��� ���� �����ײ�� ��0��ʼ �� size��С����
		bvh_node(const hittable_list& list, double time0, double time1)
			:bvh_node(list.objects,0,list.objects.size(),time0, time1){}

		//size_t ���Ϊunsigned int
		// i=start  i<end ����src_objects 
		//shared_ptr<hittable> �����hittable����������bvh_node,sphere�����κ�hittable
		bvh_node(
			const std::vector<shared_ptr<hittable>>& src_objects,
			size_t start, size_t end, double time0, double time1);

		virtual bool hit(
			const ray& r, double t_min, double t_max, hit_record& rec) const override;

		virtual bool bounding_box(double time0, double time1, aabb& output_box) const override;

	public:
		shared_ptr<hittable> left;
		shared_ptr<hittable> right;

		//�Լ��ĺ�����ô���� ��Ϊ������������
		aabb box;
};

//��Χ�ڵ�� ��Χ�м���ͷ����Լ��ĺ���
bool bvh_node:: bounding_box(double time0, double time1, aabb& output_box) const{
	output_box = box;
	return true;
}

bool bvh_node::hit(const ray&& r, double t_min, double t_max, hit_record& rec) const{
	if(!box.hit(r, t_min, t_max))
		return false;

	bool hit_left = left-> hit(r, t_min, t_max, rec);
	//������� ��ô��Ҳ�п��ܻ�����ұߣ�����һ��Ҫ�ڻ������֮ǰ�����ұߣ���t_max��ֵΪrec.t ����ʾ��󲻳���������߰�Χ��ʱ��t
	bool hit_right = right->hit(r, t_min, hit_left?rec.t:t_max, rec);
	return hit_left || hit_right;
}
#endif // ! BVH

