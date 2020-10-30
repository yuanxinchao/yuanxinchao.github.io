#ifndef CAMERA_H
#define CAMERA_H

#include "rtweekend.h"

class camera {
    public:
        camera(
			point3 lookfrom,//������ڵĵ� �������λ��
			point3 lookat,//�������ķ��� ��������ĽǶ�
			point3 vup, //һ��ָ���ϵĲο�

			double vfov,//vertical field-of-view in degrees
			double aspect_ratio
		) {
			auto theta = degrees_to_radians(vfov);//���ݽǶ��󻡶�
			auto h = tan(theta/2);//���ݻ�����߶�

			//��׶�ĸ�
			auto viewport_height = 2.0*h;

			//��׶�Ŀ�
			auto viewport_width = aspect_ratio * viewport_height;
			
			//w����������"��"�ģ���z����������ͬ
			auto w = unit_vector(lookfrom - lookat);

			//��� ���ֶ��� ��u
			auto u = unit_vector(cross(vup,w));

			//��� ���ֶ��� ��v  ���ﲻ��Ҫ��һ����Ϊw��u�ǻ��ഹֱ�ĵ�λ����
			auto v = cross(w,u);

			////����Ŀ�߱�
   //         auto aspect_ratio = 16.0 / 9.0;
			////��׶�ĸ�
   //         auto viewport_height = 2.0;
			////��׶�Ŀ�
   //         auto viewport_width = aspect_ratio * viewport_height;

			//��Ļ��������ľ���
            auto focal_length = 1.0;

			//���ԭ��
            origin = lookfrom;

			//"��Ļ"�ĽǶ�Ҳ�� uv������б
			horizontal = viewport_width * u;
            vertical = viewport_height * v;

			////ˮƽ���ҵ����� ��СΪ���
   //         horizontal = vec3(viewport_width, 0.0, 0.0);
			////��ֱ���ϵ����� ��СΪ�߶�
   //         vertical = vec3(0.0, viewport_height, 0.0);

			//���������Ļ���½ǵ�����  ����4.���ߣ���������ͱ���.md  -w������Ϊ���ָ����Ļ�м�������� �ɴ��滻�� - vec3(0, 0, focal_length)
            lower_left_corner = origin - horizontal/2 - vertical/2 - w;
            //lower_left_corner = origin - horizontal/2 - vertical/2 - vec3(0, 0, focal_length);
        }

		//u ��x����� u*horizontal
		//v ��y����� v*vertical 
		//һ��uv ȡ[0,1]
		//���ص��Ǵ�origin��ʼ �� ��Ӧ(uv)���ص����� 
        ray get_ray(double s, double t) const {
            return ray(origin, lower_left_corner + s*horizontal + t*vertical - origin);
        }

    private:
        point3 origin;
        point3 lower_left_corner;
        vec3 horizontal;
        vec3 vertical;
};
#endif