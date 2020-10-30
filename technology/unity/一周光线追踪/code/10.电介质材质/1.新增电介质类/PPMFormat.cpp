// RayTracing.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "color.h"
#include "sphere.h"
#include "camera.h"
#include "hittable_list.h"
#include "material.h"

#include <iostream>


double hit_sphere(const point3& center, double radius, const ray& r) 
{
	////根据t^2 b⋅b+2tb⋅(A−C)+(A−C)⋅(A−C)−r^2=0
	////A是射线的起点(r.origin())  C是圆心(center) b是射线方向(r.direction())

	////求A-C
	//vec3 oc = r.origin() - center;

	////求b⋅b
	//auto a = dot(r.direction(), r.direction());
	////求2b⋅(A−C)
	//auto b = 2.0 * dot(oc, r.direction());
	//auto c = dot(oc, oc) - radius * radius;
	////b^2 - 4ac  求根公式 
	//auto discriminant = b * b - 4 * a * c;
	////大于0 有解
	//if (discriminant < 0)
	//{
	//	return -1.0;
	//}
	//else
	//{
	//	//(-b±√xx)/2a  舍弃+sqrt(discriminant) 的根 因为要离相机距离最近的解
	//	return (-b - sqrt(discriminant))/(2.0 * a);
	//}

	//简化写法 −h±√(h^2−ac) /a
	vec3 oc = r.origin() - center;
    auto a = r.direction().length_squared();
    auto half_b = dot(oc, r.direction());
    auto c = oc.length_squared() - radius*radius;
    auto discriminant = half_b*half_b - a*c;

	if (discriminant < 0) {
        return -1.0;
    } else {
        return (-half_b - sqrt(discriminant) ) / a;
    }
}

color ray_color(const ray& r, const hittable& world, int depth) {
	////求与 圆心为 0,0,-1  半径为0.5的球 最近的交点 t即射线方程的t  P(t)=A+tb  b可能不是归一化的
	//auto t = hit_sphere(point3(0, 0, -1),0.5,r);
	////t大于0也就意味着射线不能反向进行碰撞检测
	//if (t > 0)
	//{
	//	//计算法线 P-C   r.at(t)即交点位置 然后归一化unit_vector
	//	vec3 N = unit_vector(r.at(t) - vec3(0,0,-1));

	//	//映射到 [0,1] 区间 
	//	return 0.5 * color(N.x() + 1,N.y()+1,N.z()+1);
	//}

	////射线单位向量 目前 r是相机到屏幕每一个像素的射线(遍历)
	//vec3 unit_direction = unit_vector(r.direction());
	////y分量从 [-1,1] 映射到 [0,1]
	//t = 0.5 * (unit_direction.y() + 1.0);
	////t越大越蓝 越小越白
	//return (1.0 - t) * color(1.0, 1.0, 1.0) + t * color(0.5, 0.7, 1.0);
	hit_record rec;

	//如果光线反射了很多次数 我们认为光线能量已经接近0了
	if(depth <= 0){
		return color(0,0,0);
	}


	//取一个击中的点
	if(world.hit(r, 0.001,infinity,rec)){
		ray scattered;
		color attenuation;
		if(rec.mat_ptr->scatter(r,rec,attenuation,scattered))
			return attenuation * ray_color(scattered,world,depth-1);
		return color(0,0,0);
	}

	//归一化方向
	//根据击中屏幕的射线y分量大小 插值白色和蓝色
	vec3 unit_direction = unit_vector(r.direction());
	auto t = 0.5*(unit_direction.y() + 1.0);//t是y[-1,1]映射到 [0,1]
	return (1.0 - t)*color(1.0,1.0,1.0) + t * color(0.5,0.7,1.0);
	//return (1.0 - t)*color(1.0,1.0,1.0) + t * color(0,0,1.0);
}


int main() {

	// Image
	//16:9的屏幕
	const auto aspect_ratio = 16.0 / 9.0;
	const int image_width = 400;
	const int image_height = static_cast<int>(image_width / aspect_ratio);
	//光线最多反射50次
	const int max_depth = 50;
	//一个像素取样100
	const int samples_per_pixel = 100;

	//World
	hittable_list world;
    auto material_ground = make_shared<lambertian>(color(0.8, 0.8, 0.0));
    auto material_center = make_shared<dielectric>(1.5);
    auto material_left   = make_shared<dielectric>(1.5);
    auto material_right  = make_shared<metal>(color(0.8, 0.6, 0.2),1.0);

    world.add(make_shared<sphere>(point3( 0.0, -100.5, -1.0), 100.0, material_ground));
    world.add(make_shared<sphere>(point3( 0.0,    0.0, -1.0),   0.5, material_center));
    world.add(make_shared<sphere>(point3(-1.0,    0.0, -1.0),   0.5, material_left));
    world.add(make_shared<sphere>(point3( 1.0,    0.0, -1.0),   0.5, material_right));




	// Camera  相机
	camera cam;

	// Render
	std::cout << "P3\n" << image_width << ' ' << image_height << "\n255\n";

	for (int j = image_height - 1; j >= 0; --j) {
		std::cerr << "\rScanlines remaining: " << j << ' ' << std::flush;
		for (int i = 0; i < image_width; ++i) {
			color pixel_color(0,0,0);

			for(int s = 0;s < samples_per_pixel; ++s){

				//random_double() 在[0,1)
				//将100条射线 随机击中在一个像素上 叠加所有颜色  然后在write_color里除以取样的数量
				//原本的方式是射线击中像素左下角作为像素的颜色

				//u x轴方向 从0到1    
				auto u = double(i + random_double()) / (image_width - 1);
				//v y轴负方向 从1到0  1在上y轴↑
				auto v = double(j + random_double()) / (image_height - 1);
			
				//重复取samples_per_pixel次射线
				ray r = cam.get_ray(u,v);
				//pixel_color 叠加上次取样结果
				pixel_color += ray_color(r, world, max_depth);
			}
			//  /samples_per_pixel
			write_color(std::cout, pixel_color, samples_per_pixel);
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
