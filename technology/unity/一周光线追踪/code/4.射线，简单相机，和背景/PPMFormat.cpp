// RayTracing.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "color.h"
#include "vec3.h"
#include "ray.h"

#include <iostream>

color ray_color(const ray& r) {
	//射线单位向量
    vec3 unit_direction = unit_vector(r.direction());
	//y分量从 [-1,1] 映射到 [0,1]
    auto t = 0.5*(unit_direction.y() + 1.0);
	//t越大越蓝 越小越白
    return (1.0-t)*color(1.0, 1.0, 1.0) + t*color(0.5, 0.7, 1.0);
}

int main() {

    // Image
	//16:9的屏幕
    const auto aspect_ratio = 16.0 / 9.0;
    const int image_width = 400;
    const int image_height = static_cast<int>(image_width / aspect_ratio);

	// Camera  相机
	//视锥的高
    auto viewport_height = 2.0;
	//视锥的宽
    auto viewport_width = aspect_ratio * viewport_height;

	//摄像机与图片的距离
    auto focal_length = 1.0;

	//射线原点
    auto origin = point3(0, 0, 0);
    auto horizontal = vec3(viewport_width, 0, 0);
    auto vertical = vec3(0, viewport_height, 0);
	//摄像机到屏幕左下角的射线
    auto lower_left_corner = origin - horizontal/2 - vertical/2 - vec3(0, 0, focal_length);

    // Render
    std::cout << "P3\n" << image_width << ' ' << image_height << "\n255\n";

    for (int j = image_height-1; j >= 0; --j) {
		std::cerr << "\rScanlines remaining: " << j << ' ' << std::flush;
        for (int i = 0; i < image_width; ++i) {
			//u x轴方向 从0到1 
            auto u = double(i) / (image_width-1);
			//v y轴负方向 从1到0  
            auto v = double(j) / (image_height-1);
			//新建射线，原点(相机)为起点 方向为起点到屏幕当前的像素
            ray r(origin, lower_left_corner + u*horizontal + v*vertical - origin);

			//
            color pixel_color = ray_color(r);
            write_color(std::cout, pixel_color);
        }
    }
	std::cerr << "\nDone.\n";
}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门使用技巧: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
