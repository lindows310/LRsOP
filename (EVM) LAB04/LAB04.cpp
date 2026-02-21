#define _USE_MATH_DEFINES
#include <cmath>
#include <iostream>

double calc_asm(double a, double b)
{
    // X=sin(a)+cos(a)+tg(a)+ctg(a)+Pi*(a*a-b)/(-b*b-a)
    double res, Pi = M_PI; bool error = 0;
    __asm {
                                   //   st0            st1            st2            st3        st4
        finit     
        fldz                       //    0
        fld qword ptr[a]           //    a              0
        fld qword ptr[a]           //    a              a              0
        fsincos                    //   cosa           sina            a              0
        fcomi st(0), st(3)         //   cosa не равно нулю - ограничение tana (tana = sina/cosa)
        je error_exit
        fxch st(1)                 //   sina           cosa            a              0
        fcomi st(0), st(3)         //   sina не равно нулю - ограничение ctga (ctga = cosa/sina)
        je error_exit
        fxch st(1)                 //   cosa           sina            a              0
        fld1                       //    1             cosa           sina            a          0
        fdivrp st(1), st(0)        //   1/cosa         sina            a              0
        fdiv st(0), st(1)          // ctga+tana        sina            a              0
        fxch st(2)                 //    a             sina        ctga+tana          0
        fcos                       //   cosa           sina        ctga+tana          0
        faddp st(1), st(0)         // cosa+sina      ctga+tana         0
        faddp st(1), st(0)         //ctga+tana+cosa+sina 0
        fld qword ptr[a]           //    a      ctga+tana+cosa+sina    0
        fmul st(0), st(0)          //   a*a     ctga+tana+cosa+sina    0
        fld qword ptr[b]           //    b             a*a     ctga+tana+cosa+sina    0
        fsubp st(1), st(0)         //   a*a-b   ctga+tana+cosa+sina    0
        fmul Pi                    // Pi(a*a-b) ctga+tana+cosa+sina    0
        fld qword ptr[b]           //    b          Pi(a*a-b)  ctga+tana+cosa+sina    0
        fmul st(0), st(0)          //   b*b         Pi(a*a-b)  ctga+tana+cosa+sina    0
        fld qword ptr[a]           //    a             b*b         Pi(a*a-b) ctga+tana+cosa+sina 0
        faddp st(1), st(0)         //  b*b+a        Pi(a*a-b)  ctga+tana+cosa+sina    0
        fchs                       // -b*b-a        Pi(a*a-b)  ctga+tana+cosa+sina    0
        fcomi st(0), st(3)
        je error_exit
        fdivp st(1), st(0)         // Pi(a*a-b)/(-b*b-a) ctga+tana+cosa+sina          0
        faddp st(1), st(0)         // Результат: ctga+tana+cosa+sina + Pi(a*a-b)/(-b*b-a) - лежит в регистре st(0)
        fstp res
        jmp end

        error_exit:
        mov error, 1
        end:
    }
    if (error == 1)
        throw 0;
    return res;
}
double calc_cpp(int a, int b)
{
    return sin(a) + cos(a) + 1 / tan(a) + tan(a) + M_PI * (a * a - b) / (-b * b - a);
}
int main()
{
    setlocale(LC_ALL, "Russian");

    printf("Ассемблер. Лабораторная работа № 4. Выполнил студент группы 6101-020302D Абросимов Артём.\n");
    printf("========================================================================================\n");
    printf("Вариант 1. Задание: реализация функции X=sin(a)+cos(a)+tg(a)+ctg(a)+Pi*(a*a-b)/(-b*b-a)\n");

    double a, b;
    printf("Введите a: "); std::cin >> a;
    printf("Введите b: "); std::cin >> b;

    try
    {
        printf("\nРезультат работы программы на asm: %f", calc_asm(a, b));
        printf("\nРезультат работы программы на cpp: %f", calc_cpp(a, b));
    }
    catch (...)
    {
        printf("Ошибка. Было вызвано деление на ноль.");
    }
}