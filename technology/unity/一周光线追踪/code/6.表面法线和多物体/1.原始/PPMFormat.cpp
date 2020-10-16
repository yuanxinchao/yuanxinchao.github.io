// RayTracing.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "color.h"
#include "vec3.h"
#include "ray.h"

#include <iostream>
double hit_sphere(const point3& center, double radius, const ray& r) 
{
	//根据t^2 b⋅b+2tb⋅(A−C)+(A−C)⋅(A−C)−r^2=0
	//A是射线的起点(r.origin())  C是圆心(center) b是射线方向(r.direction())

	//求A-C
	vec3 oc = r.origin() - center;

	//求b⋅b
	auto a = dot(r.direction(), r.direction());
	//求2b⋅(A−C)
	auto b = 2.0 * dot(oc, r.direction());
	auto c = dot(oc, oc) - radius * radius;
	//b^2 - 4ac  求根公式 
	auto discriminant = b * b - 4 * a * c;
	//大于0 有解
	if (discriminant < 0)
	{
		return -1.0;
	}
	else
	{
		//(-b±√xx)/2a  舍弃+sqrt(discriminant) 的根 因为要离相机距离最近的解
		return (-b - sqrt(discriminant))/(2.0 * a);
	}
}

color ray_color(const ray& r) {
	//求与 圆心为 0,0,-1  半径为0.5的球 最近的交点 t即射线方程的t  P(t)=A+tb  b可能不是归一化的
	auto t = hit_sphere(point3(0, 0, -1),0.5,r);
	if (t > 0)
	{
		//计算法线 P-C   r.at(t)即交点位置 然后归一化unit_vector
		vec3 N = unit_vector(r.at(t) - vec3(0,0,-1));

		//映射到 [0,1] 区间 
		return 0.5 * color(N.x() + 1,N.y()+1,N.z()+1);
	}

	//射线单位向量 目前 r是相机到屏幕每一个像素的射线(遍历)
	vec3 unit_direction = unit_vector(r.direction());
	//y分量从 [-1,1] 映射到 [0,1]
	t = 0.5 * (unit_direction.y() + 1.0);
	//t越大越蓝 越小越白
	return (1.0 - t) * color(1.0, 1.0, 1.0) + t * color(0.5, 0.7, 1.0);
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
	auto lower_left_corner = origin - horizontal / 2 - vertical / 2 - vec3(0, 0, focal_length);

	// Render
	std::cout << "P3\n" << image_width << ' ' << image_height << "\n255\n";

	for (int j = image_height - 1; j >= 0; --j) {
		std::cerr << "\rScanlines remaining: " << j << ' ' << std::flush;
		for (int i = 0; i < image_width; ++i) {
			//u x轴方向 从0到1 
			auto u = double(i) / (image_width - 1);
			//v y轴负方向 从1到0  
			auto v = double(j) / (image_height - 1);
			//新建射线，原点(相机)为起点 方向为起点到屏幕当前的像素
			ray r(origin, lower_left_corner + u * horizontal + v * vertical - origin);

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
