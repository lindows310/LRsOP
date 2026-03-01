#include <iostream>
#include <stdio.h>
#include <string>

int sum_asm(int* arr, int lim, int arr_length)
{
    if (arr_length <= 0)
        return 0;

    int sum_result = 0;

    __asm {
        xor eax, eax             // EAX очищен (сумма)
        xor edi, edi             // EDI очищен (счетчик)
        mov ebx, arr             // в EBX - ссылка на arr
        mov ecx, arr_length      // в ECX - длина arr

        begin_loop:
        cmp edi, ecx             // EDI == ECX - переход (счетчик равен длине массива)
        je end_loop              // Переход в конец цикла
        mov esi, [ebx + edi * 4] // В ESI - arr[EDI] - элемент массива
        cmp esi, 0               // В ESI - ноль, сумма не меняется, переход в следующий цикл (число отрицательное - не выполнено условие)
        jle next_element         // Следующая итерация
        cmp esi, lim             // ESI < lim => не выполняется условие, переход в следующей итерации
        jl next_element          // Следующая итерация
        mov edx, esi             // В EDX - ESI (arr[EDI])
        imul esi, esi            // В ESI - (arr[EDI])^2
        imul esi, edx            // В ESI - (arr[EDI])^3
        add eax, esi             // В EAX - сумма элементов

        next_element:
        inc edi                  // EDI = EDI + 1
        jmp begin_loop           // Переход в начало цикла

        end_loop:
        mov sum_result, eax      // sum_result = EAX (итоговая сумма)
    }

    return sum_result;
}

int sum_cpp(int* nums, int lim, int len)
{
    int sum_result = 0;
    for (int i = 0; i < len; i++) {
        if (nums[i] >= lim && nums[i] > 0) {
            sum_result += nums[i] * nums[i] * nums[i];
        }
    }
    return sum_result;
}

int main()
{
    setlocale(LC_ALL, "Russian");
    printf("Ассемблер. Лабораторная работа №3. Выполнил студент группы 6101-020302D Абросимов Артём\n========================================================================================\n");
    printf("Вариант 1. Задание: в одномерном массиве A={a[i]} целых чисел вычислить сумму кубов всех\nположительных элементов массива, удовлетворяющих условию: a[i] >=b. \n");

    int length, b; bool flag = true; bool run = true;
    printf("Введите b: "); std::cin >> b;

    while (run)
    {
        std::string option = "";
        printf("\nВведите длину массива: "); std::cin >> length;

        if (length <= 0)
        {
            printf("Ошибка. Повторите ввод (1) или выйдите из программы (любой символ)\n");
            std::cin >> option;
            if (option == "1")
                continue;
            else
            {
                flag = false;
                printf("Выход из программы...");
            }
        }
        else
            run = false;
    }

    if (flag)
    {
        int* nums_asm = new int[length];
        int* nums_cpp = new int[length];

        for (int i = 0; i < length; i++) {
            printf("Введите элемент %d: ", i + 1); std::cin >> nums_asm[i];
            nums_cpp[i] = nums_asm[i];
        }

        printf("\nРезультат вычислений на C++: %d", sum_cpp(nums_cpp, b, length));
        printf("\nРезультат вычислений на asm: %d", sum_asm(nums_asm, b, length));
    }
}   