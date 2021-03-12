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

			//
			double aspect_ratio,
			double aperture,//С��ֱ��
			double focus_dist,//��Ļ��������ľ���

			double _time0 = 0,
			double _time1 = 0
		) {
			auto theta = degrees_to_radians(vfov);//���ݽǶ��󻡶�
			auto h = tan(theta/2);//���ݻ�����߶�

			//��׶�ĸ�
			auto viewport_height = 2.0*h;

			//��׶�Ŀ�
			auto viewport_width = aspect_ratio * viewport_height;

			
			//w����������"��"�ģ���z����������ͬ
			w = unit_vector(lookfrom - lookat);

			//��� ���ֶ��� ��u
			u = unit_vector(cross(vup,w));

			//��� ���ֶ��� ��v  ���ﲻ��Ҫ��һ����Ϊw��u�ǻ��ഹֱ�ĵ�λ����
			v = cross(w,u);

			////����Ŀ�߱�
   //         auto aspect_ratio = 16.0 / 9.0;
			////��׶�ĸ�
   //         auto viewport_height = 2.0;
			////��׶�Ŀ�
   //         auto viewport_width = aspect_ratio * viewport_height;

			

			//���ԭ��
            origin = lookfrom;

			//"��Ļ"�ĽǶ�Ҳ�� uv������б
			////��Ļ��������ľ��� ԽԶ ��Ļ���Խ��
			horizontal = focus_dist * viewport_width * u;
            vertical = focus_dist *viewport_height * v;

			////ˮƽ���ҵ����� ��СΪ���
   //         horizontal = vec3(viewport_width, 0.0, 0.0);
			////��ֱ���ϵ����� ��СΪ�߶�
   //         vertical = vec3(0.0, viewport_height, 0.0);

			//���������Ļ���½ǵ�����  ����4.���ߣ���������ͱ���.md  -w������Ϊ���ָ����Ļ�м�������� �ɴ��滻�� - vec3(0, 0, focal_length)
			//-w ��Ҫ�˶Խ�����
            lower_left_corner = origin - horizontal/2 - vertical/2 - focus_dist * w;

			lens_radius = aperture /2;
            //lower_left_corner = origin - horizontal/2 - vertical/2 - vec3(0, 0, focal_length);


			time0 = _time0;
			time1 = _time1;
        }

		//u ��x����� u*horizontal
		//v ��y����� v*vertical 
		//һ��uv ȡ[0,1]
		//���ص��Ǵ�origin��ʼ �� ��Ӧ(uv)���ص����� 

		//�������ɢ�䣬 ������ƫ��offset��ĵ� ������ʼ�㵱ȻҲ��ƫ�ƺ�� ����Ҫ �ټ�offset
        ray get_ray(double s, double t) const {
			vec3 rd = lens_radius * random_in_unit_disk();
			vec3 offset = u * rd.x() + v * rd.y();


            return ray(origin + offset, //���
				lower_left_corner + s*horizontal + t*vertical - origin - offset, //����
				random_double(time0,time1)); //���һ��ʱ��timex ����ʱ���timex������ʱ�����ȡ��������λ��  ������������"����"��ʱ��timex������
        }

    private:
        point3 origin;
        point3 lower_left_corner;
        vec3 horizontal;
        vec3 vertical;
		vec3 u, v, w;
		double lens_radius;
		double time0,time1;//���� ����/�ر�  ��ʱ��
};
#endif