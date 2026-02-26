#define _USE_MATH_DEFINES
#include <iostream>
#include <cmath>
#include <iomanip>

double func_asm(double x, double y)
{
    double c1 = 3.0 / 2.0; double c2 = 5.0 / 4.0; double c3 = 4.0 / 3.0; double c4 = 3.0; double const_16 = 16.0;  double const_4 = 4.0;
    double result; bool error;

    // Задание pow(x, 3 / 2) + (4 / 3) * atan(4 / x) - (3 / 4) * asin(x / 4) - log(3 * x / 4) + 4 * pow(M_E, x / 3) + pow(4 / 3, y) (-)
    // (-) sqrt(pow(M_E, cos(3 * y / 2) + pow(M_E, sin(5 * y / 4)))) + 1 / sin(y);
    __asm {
                             //          st(0)                          st(1)                  st(2)                       st(3)                   st(4)
                             //                            ---------------------- ПРОВЕРКА ИСКЛЮЧЕНИЙ ---------------------------
        finit                //           0                               0                      0                           0                       0
        fldz                 //           0                               0                      0                           0                       0
        fld const_4          //           4                               0                      0                           0                       0
        fld x                //           x                               4                      0                           0                       0
        fcomi st(0), st(2)   // !!!! Проверка ОДЗ натурального логарифма (ln(3x/4)), если число меньше нуля - оно не подходит. Переход.
        jbe error_exit       // !!!! Переход
        fabs                 //          |x|                              4                      0                           0                       0
        fcomi st(0), st(1)   // !!!! Проверка sqrt(16 - x^2) - представления arcsin(x/4) по формуле arcsin(x) = arctg(x/sqrt(1-x^2))
        jae error_exit       // !!!! Переход
        fld y                //           y                              |x|                     1                           0                       0
        fsin                 //         sin(y)                           |x|                     1                           0                       0
        fcomi st(0), st(3)   // !!!! Проверка sin(y) != 0. Ограничение cosec(y) (cosec(y) = 1/sin(y))
        je error_exit        // !!!! Переход
                             //                            -------------------------- ОСНОВНОЙ КОД -------------------------------
        finit                //           0                               0                      0                           0                       0
        fldz                 //           0                               0                      0                           0                       0
        fldl2e               //         log2(e)                           0                      0                           0                       0 (начало вычисления -sqrt(e^(sin(5y/4))+e^(cos(3y/2))))
        fld y                //           y                             log2(e)                  0                           0                       0
        fld c1               //          3/2                              y                    log2(e)                       0                       0
        fmulp st(1), st(0)   //          3y/2                           log2(e)                  0                           0                       0
        fcos                 //        cos(3y/2)                        log2(e)                  0                           0                       0
        fmulp st(1), st(0)   //     log2(e)*cos(3y/2)                     0                      0                           0                       0
        fld st(0)            //     log2(e)*cos(3y/2)              log2(e)*cos(3y/2)             0                           0                       0
        frndint              //     [log2(e)*cos(3y/2)]            log2(e)*cos(3y/2)             0                           0                       0
        fsub st(1), st(0)    //     [log2(e)*cos(3y/2)]           {log2(e)*cos(3y/2)}            0                           0                       0
        fxch st(1)           //     {log2(e)*cos(3y/2)}           [log2(e)*cos(3y/2)]            0                           0                       0
        f2xm1                //     2^{log2(e)*cos(3y/2)}-1       [log2(e)*cos(3y/2)]            0                           0                       0
        fld1                 //            1                    2^{log2(e)*cos(3y/2)}-1    [log2(e)*cos(3y/2)]               0                       0
        faddp st(1), st(0)   //     2^{log2(e)*cos(3y/2)}         [log2(e)*cos(3y/2)]            0                           0                       0
        fscale               //     e^(cos(3y/2))                 [log2(e)*cos(3y/2)]            0                           0                       0
        fxch st(1)           //     [log2(e)*cos(3y/2)]             e^(cos(3y/2))                0                           0                       0
        fstp st(0)           //       e^(cos(3y/2))                       0                      0                           0                       0
        fldl2e               //         log2(e)                     e^(cos(3y/2))                0                           0                       0
        fld y                //            y                            log2(e)              e^(cos(3y/2))                   0                       0
        fld c2               //           5/4                             y                   log2(e)                   e^(cos(3y/2))                0
        fmulp st(1), st(0)   //           5y/4                          log2(e)              e^(cos(3y/2))                   0                       0
        fsin                 //         sin(5y/4)                       log2(e)              e^(cos(3y/2))                   0                       0
        fmulp st(1), st(0)   //     log2(e)*sin(5y/4)                e^(cos(3y/2))               0                           0                       0
        fld st(0)            //     log2(e)*sin(5y/4)              log2(e)*sin(5y/4)         e^(cos(3y/2))                   0                       0
        frndint              //     [log2(e)*sin(5y/4)]            log2(e)*sin(5y/4)         e^(cos(3y/2))                   0                       0
        fsub st(1), st(0)    //     [log2(e)*sin(5y/4)]            {log2(e)*sin(5y/4)}       e^(cos(3y/2))                   0                       0 
        fxch st(1)           //     {log2(e)*sin(5y/4)}            [log2(e)*sin(5y/4)]       e^(cos(3y/2))                   0                       0
        f2xm1                //    2^{log2(e)*sin(5y/4)}-1         [log2(e)*sin(5y/4)]       e^(cos(3y/2))                   0                       0
        fld1                 //             1                    2^{log2(e)*sin(5y/4)}-1  [log2(e)*sin(5y/4)]           e^(cos(3y/2))                0
        faddp st(1), st(0)   //    2^{log2(e)*sin(5y/4)}           [log2(e)*sin(5y/4)]       e^(cos(3y/2))                   0                       0
        fscale               //        e^(sin(5y/4))               [log2(e)*sin(5y/4)]       e^(cos(3y/2))                   0                       0
        fxch st(1)           //     [log2(e)*sin(5y/4)]             e^(sin(5y/4))            e^(cos(3y/2))                   0                       0
        fstp st(0)           //        e^(sin(5y/4))                e^(cos(3y/2))                0                           0                       0
        faddp st(1), st(0)   //   e^(sin(5y/4))+e^(cos(3y/2))             0                      0                           0                       0
                             //
        fcomi st(0), st(1)   // !!!! Проверка нуля. e^(sin(5y/4))+e^(cos(3y/2)) должно быть больше нуля (ограничение квадратного корня)
        je error_exit        // !!!! Переход
                             //
        fsqrt                //  (e^(sin(5y/4))+e^(cos(3y/2)))^(1/2)      0                      0                           0                       0
        fchs                 // -(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2)      0                      0                           0                       0
        fld x                //             x              -(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2)  0                           0                       0 (начало вычисления x^(3/2))
        fld st(0)            //             x                             x         -(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2)     0                       0
        fsqrt                //          x^(1/2)                          x         -(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2)     0                       0
        fmulp st(1), st(0)   //          x^(3/2)           -(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2)  0                           0                       0
        faddp st(1), st(0)   //x^(3/2)-(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2)0                      0                           0                       0
        fld x                //             x       x^(3/2)-(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2)  0                           0                       0 (начало вычисления (4/3)*arctg(x/4)
        fld const_4          //             4                             x    x^(3/2)-(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2)   0                       0
        fpatan               //         arctg(x/4)      x^(3/2)-(e^(sin(5y/4))+e^(cos(3y/2)))^2  0                           0                       0
        fld c3               //            4/3                        arctg(x/4) x^(3/2)-(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2) 0                       0
        fmulp st(1), st(0)   //     (4/3)(arctg(x/4)) x^(3/2)-(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2) 0                          0                       0
                             //
        faddp st(1), st(0)   // !!!! Промежуточный результат: ST(0) = (4/3)(arctg(x/4))+x^(3/2)-(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2) !!!!
        sub esp, 10          // Число в ST(0) - 80-битное число расширенной точности с плавающей точкой. Размер - 10 байт. Вычитаем из памяти стека 10 байт
        fstp tbyte ptr [esp] // В памяти стека теперь лежит (4/3)(arctg(x/4))+x^(3/2)-(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2)
                             //
                             //             0                             0                      0                           0                       0
        fld1                 //             1                             0                      0                           0                       0 (начало вычисления ln(3x/4))
        fld x                //             x                             1                      0                           0                       0
        fld c4               //             3                             x                      1                           0                       0
        fmulp st(1), st(0)   //             3x                            1                      0                           0                       0
        fyl2x                //          log2(3x)                         0                      0                           0                       0
        fld1                 //             1                          log2(3x)                  0                           0                       0
        fadd st(0), st(0)    //             2                          log2(3x)                  0                           0                       0
        fsubp st(1), st(0)   //          log2(3x)-2                       0                      0                           0                       0
        fldl2e               //          log2(e)                       log2(3x)-2                0                           0                       0
        fdivp st(1), st(0)   //          ln(3x/4)                         0                      0                           0                       0
        fld1                 //             1                          ln(3x/4)                  0                           0                       0
        fld c3               //            4/3                            1                   ln(3x/4)                       0                       0 (начало вычисления (4/3)^y
        fyl2x                //         log2(4/3)                      ln(3x/4)                  0                           0                       0
        fld y                //             y                          log2(4/3)              ln(3x/4)                       0                       0
        fmulp st(1), st(0)   //        y*log2(4/3)                     ln(3x/4)                  0                           0                       0
        fld st(0)            //        y*log2(4/3)                    y*log2(4/3)             ln(3x/4)                       0                       0
        frndint              //       [y*log2(4/3)]                   y*log2(4/3)             ln(3x/4)                       0                       0
        fsub st(1), st(0)    //       [y*log2(4/3)]                  {y*log2(4/3)}            ln(3x/4)                       0                       0
        fxch st(1)           //       {y*log2(4/3)}                  [y*log2(4/3)]            ln(3x/4)                       0                       0
        f2xm1                //      2^{y*log2(4/3)}-1               [y*log2(4/3)]            ln(3x/4)                       0                       0
        fld1                 //             1                      2^{log2(4/3)}-1          [y*log2(4/3)]                 ln(3x/4)                   0
        faddp st(1), st(0)   //       2^{y*log2(4/3)}                [y*log2(4/3)]            ln(3x/4)                       0                       0
        fscale               //          (4/3)^y                     [y*log2(4/3)]            ln(3x/4)                       0                       0
        fstp st(1)           //          (4/3)^y                       ln(3x/4)                  0                           0                       0
        fsubrp st(1), st(0)  //      (4/3)^y-ln(3x/4)                     0                      0                           0                       0
        fldl2e               //          log2(e)                    (4/3)^y-ln(3x/4)             0                           0                       0
        fld x                //             x                           log2(e)           (4/3)^y-ln(3x/4)                   0                       0 (начало вычисления 4e^(x/3))
        fld c4               //             3                             x                   log2(e)                 (4/3)^y+ln(3x/4)               0
        fdivp st(1), st(0)   //            x/3                          log2(e)           (4/3)^y-ln(3x/4)                   0                       0
        fmulp st(1), st(0)   //        (x/3)log2(e)                 (4/3)^y-ln(3x/4)             0                           0                       0
        fld st(0)            //        (x/3)log2(e)                  (x/3)log2(e)         (4/3)^y-ln(3x/4)                   0                       0
        frndint              //        [(x/3)log2(e)]                (x/3)log2(e)         (4/3)^y-ln(3x/4)                   0                       0
        fsub st(1), st(0)    //        [(x/3)log2(e)]                {(x/3)log2(e)}       (4/3)^y-ln(3x/4)                   0                       0            
        fxch st(1)           //        {(x/3)log2(e)}                [(x/3)log2(e)]       (4/3)^y-ln(3x/4)                   0                       0
        f2xm1                //     2^{(x/3)log2(e)}-1               [(x/3)log2(e)]       (4/3)^y-ln(3x/4)                   0                       0
        fld1                 //             1                      2^{(x/3)log2(e)}-1     [(x/3)log2(e)]               (4/3)^y-ln(3x/4)              0
        faddp st(1), st(0)   //       2^{(x/3)log2(e)}               [(x/3)log2(e)]       (4/3)^y-ln(3x/4)                   0                       0
        fscale               //           e^(x/3)                   (4/3)^y-ln(3x/4)             0                           0                       0
        fstp st(1)           //           e^(x/3)                   (4/3)^y-ln(3x/4)             0                           0                       0
        fld c3               //            4/3                           e^(x/3)          (4/3)^y-ln(3x/4)                   0                       0
        fld c4               //             3                             4/3                 e^(x/3)                  (4/3)^y-ln(3x/4)              0
        fmulp st(1), st(0)   //             4                            e^(x/3)          (4/3)^y-ln(3x/4)                   0                       0
        fmulp st(1), st(0)   //          4e^(x/3)                  (4/3)^y-ln(3x/4)              0                           0                       0
        faddp st(1), st(0)   //   4e^(x/3)+(4/3)^y-ln(3x/4)                0                     0                           0                       0
        fld const_16         //             16                  4e^(x/3)+(4/3)^y-ln(3x/4)        0                           0                       0 (начало вычисления -arcsin(x/4))
        fld x                //             x                              16         4e^(x/3)+(4/3)^y-ln(3x/4)              0                       0
        fmul st(0), st(0)    //            x^2                             16         4e^(x/3)+(4/3)^y-ln(3x/4)              0                       0
        fsubp st(1), st(0)   //          16-x^2                 4e^(x/3)+(4/3)^y-ln(3x/4)        0                           0                       0
        fld x                //             x                            16-x^2       4e^(x/3)+(4/3)^y-ln(3x/4)              0                       0
        fxch st(1)           //          16-x^2                            x          4e^(x/3)+(4/3)^y-ln(3x/4)              0                       0
        fsqrt                //       (16-x^2)^(1/2)                       x          4e^(x/3)+(4/3)^y-ln(3x/4)              0                       0
        fpatan               //         arcsin(x/4)             4e^(x/3)+(4/3)^y-ln(3x/4)        0                           0                       0
        fld c4               //             3                          arcsin(x/4)    4e^(x/3)+(4/3)^y-ln(3x/4)              0                       0
        fld const_4          //             4                              3                 arcsin(x/4)          4e^(x/3)+(4/3)^y-ln(3x/4)          0
        fdivp st(1), st(0)   //            3/4                         arcsin(x/4)    4e^(x/3)+(4/3)^y-ln(3x/4)              0                       0
        fmulp st(1), st(0)   //      (3/4)arcsin(x/4)           4e^(x/3)+(4/3)^y-ln(3x/4)        0                           0                       0
        fsubp st(1), st(0)   // 4e^(x/3)+(4/3)^y-ln(3x/4)-(3/4)arcsin(x/4) 0                     0                           0                       0
            fld y                //             y        4e^(x/3)+(4/3)^y-ln(3x/4)-(3/4)arcsin(x/4)  0                           0                       0 (начало вычисления cosec(y)
        fsin                 //           sin(y)                           0                     0                           0                       0
        fld1                 //             1                            sin(y)  4e^(x/3)+(4/3)^y-ln(3x/4)-(3/4)arcsin(x/4)  0                       0
        fdivrp st(1), st(0)  //         cosec(y)     4e^(x/3)+(4/3)^y-ln(3x/4)-(3/4)arcsin(x/4)  0                           0                       0
        faddp st(1), st(0)   //cosec+4e^(x/3)+(4/3)^y-ln(3x/4)-(3/4)arcsin(x/4) 0                0                           0                       0
                             //
        fld tbyte ptr [esp]  //  ST(1) = (4/3)(arctg(x/4))+x^(3/2)-(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2), ST(2) = cosec+4e^(x/3)+(4/3)^y-ln(3x/4)-arcsin(x/4)
        faddp st(1), st(0)   //  ST(1) = (4/3)(arctg(x/4))+x^(3/2)-(e^(sin(5y/4))+e^(cos(3y/2)))^(1/2) + cosec+4e^(x/3)+(4/3)^y-ln(3x/4)-arcsin(x/4)
        add esp, 10          //  Память стека восстановлена
        fstp result
        jmp end
        
        error_exit:
        mov error, 1

        end:
    }
    if (error == 1)
        throw 0;
    return result;
}
double func_cpp(double x, double y)
{
    double term1 = pow(x, 3.0 / 2.0);
    double term2 = (4.0 / 3.0) * atan(x / 4.0);
    double term3 = -((3.0 / 4.0) * asin(x / 4.0));
    double term4 = -log(3.0 * x / 4.0);
    double term5 = 4.0 * pow(M_E, x / 3.0);
    double term6 = pow(4.0 / 3.0, y);
    double term7 = -sqrt(pow(M_E, cos(3.0 * y / 2.0)) + pow(M_E, sin(5.0 * y / 4.0)));
    double term8 = 1.0 / sin(y);

    double result = term1 + term2 + term3 + term4 + term5 + term6 + term7 + term8;
    if (std::isnan(result))
        throw 0;
    return result;
}

int main()
{
    setlocale(LC_ALL, "Russian");
    printf("Ассемблер. Лабораторная работа № 5. Выполнил студент группы 6101-020302D Абросимов Артём\n");
    printf("========================================================================================");
    printf("\nЗадание, ВАРИАНТ 1: x^(3/2) + (4/3)*arctg(x/4) - (3/4)*arcsin(x/4) - ln(3x/4) + 4e^(x/3) (+)\n(+) (4/3)^y - sqrt(e^(cos(3y/2)) + e^(sin(5y/4)) + cosec(y)\n");
    double x; double y; double x0; double x1; double y0; double y1;
    double hx; double hy; int num = 1;

    printf("\nВведите начальное значение отрезка значений x0: "); std::cin >> x0;
    printf("Введите конечное значение отрезка значений x1: ");  std::cin >> x1;

    printf("\nВведите начальное значение отрезка значений y0: "); std::cin >> y0;
    printf("Введите конечное значение отрезка значений y1: ");  std::cin >> y1;

    printf("\nВведите величину шага hx: "); std::cin >> hx;
    printf("Введите величину шага hy: "); std::cin >> hy;

    printf("========================================================================================");
    printf("\n(Прочерк - значение неопределенно)\n");
    printf("\nNo \t x \t y \t f(x,y) asm \t f(x,y) c++\n");

    for (double i = x0, j = y0; i <= x1 && j <= y1; i += hx, j += hy)
    {
        std::cout << std::right << std::setw(3) << num << "\t" << std::right << std::setw(3) << i << "\t" << std::right << std::setw(3) << j << "\t";
        try
        {
            std::cout << std::setw(10) << func_asm(i, j) << "\t";
        }
        catch (...)
        {
            std::cout << std::setw(10) << "-" << "\t";
        }
        try
        {
            std::cout << std::setw(10) << func_cpp(i, j) << "\n";
        }
        catch (...)
        {
            std::cout << std::setw(10) << "-" << "\n";
        }
        num++;
    }
}
