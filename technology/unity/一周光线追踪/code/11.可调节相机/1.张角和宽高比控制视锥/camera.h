#ifndef CAMERA_H
#define CAMERA_H

#include "rtweekend.h"

class camera {
    public:
        camera(
			double vfov,//vertical field-of-view in degrees
			double aspect_ratio
		) {
			auto theta = degrees_to_radians(vfov);//���ݽǶ��󻡶�
			auto h = tan(theta/2);//���ݻ�����߶�

			//��׶�ĸ�
			auto viewport_height = 2.0*h;

			//��׶�Ŀ�
			auto viewport_width = aspect_ratio * viewport_height;

			////����Ŀ�߱�
   //         auto aspect_ratio = 16.0 / 9.0;
			////��׶�ĸ�
   //         auto viewport_height = 2.0;
			////��׶�Ŀ�
   //         auto viewport_width = aspect_ratio * viewport_height;

			//��Ļ��������ľ���
            auto focal_length = 1.0;

			//���ԭ��
            origin = point3(0, 0, 0);
			//ˮƽ���ҵ����� ��СΪ���
            horizontal = vec3(viewport_width, 0.0, 0.0);
			//��ֱ���ϵ����� ��СΪ�߶�
            vertical = vec3(0.0, viewport_height, 0.0);

			//���������Ļ���½ǵ�����
            lower_left_corner = origin - horizontal/2 - vertical/2 - vec3(0, 0, focal_length);
        }

		//u ��x����� u*horizontal
		//v ��y����� v*vertical 
		//һ��uv ȡ[0,1]
		//���ص��Ǵ�origin��ʼ �� ��Ӧ(uv)���ص����� 
        ray get_ray(double u, double v) const {
            return ray(origin, lower_left_corner + u*horizontal + v*vertical - origin);
        }

    private:
        point3 origin;
        point3 lower_left_corner;
        vec3 horizontal;
        vec3 vertical;
};
#endif