#ifndef CAMERA_H
#define CAMERA_H

#include "rtweekend.h"

class camera {
    public:
        camera(
			double vfov,//vertical field-of-view in degrees
			double aspect_ratio
		) {
			auto theta = degrees_to_radians(vfov);//根据角度求弧度
			auto h = tan(theta/2);//根据弧度求高度

			//视锥的高
			auto viewport_height = 2.0*h;

			//视锥的宽
			auto viewport_width = aspect_ratio * viewport_height;

			////相机的宽高比
   //         auto aspect_ratio = 16.0 / 9.0;
			////视锥的高
   //         auto viewport_height = 2.0;
			////视锥的宽
   //         auto viewport_width = aspect_ratio * viewport_height;

			//屏幕距离相机的距离
            auto focal_length = 1.0;

			//相机原点
            origin = point3(0, 0, 0);
			//水平向右的向量 大小为宽度
            horizontal = vec3(viewport_width, 0.0, 0.0);
			//垂直向上的向量 大小为高度
            vertical = vec3(0.0, viewport_height, 0.0);

			//摄像机到屏幕左下角的射线
            lower_left_corner = origin - horizontal/2 - vertical/2 - vec3(0, 0, focal_length);
        }

		//u 沿x轴→走 u*horizontal
		//v 沿y轴↑走 v*vertical 
		//一般uv 取[0,1]
		//返回的是从origin开始 到 对应(uv)像素的射线 
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