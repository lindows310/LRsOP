#include <iostream>
#include <stdio.h>
#include <string>
#include <vector>
#include <sstream>

int sum_asm(std::vector<int> arr, int lim)
{
    int sum_result;
    if (arr.size() == 0)
        return 0;
    int arr_size = arr.size() - 1;

    int* vecdata = &arr[0];
                                   // Задание: 1)	В одномерном массиве A={a[i]} целых чисел вычислить сумму кубов всех положительных элементов массива, удовлетворяющих условию: a[i] >=b.
    __asm {
        xor eax, eax
        xor esi, esi               // буфер элементов массива
        xor edi, edi               // регистр, содержащий счетчик
        mov ebx, vecdata               // EBX содержит ссылку на arr{}
        mov ecx, arr_size          // ECX содержит размер массива arr{}
        jcxz exit_1                // ECX (arr_size) = 0 - выходим из программы

        begin_loop:
        mov esi, [ebx + 4*edi]
        cmp esi, lim               // arr[EDI] ? lim
        jb end_loop                // Переход осуществляется, если ESI (arr[EDI]) меньше lim
        imul esi, esi              // ESI = (arr[EDI])^2
        add eax, esi               // EAX = EAX + (arr[EDI])^2
        cmp ecx, edi               // arr_size ? 4*EDI
        jna exit_1                 // В случае, если сдвиг адреса начала массива (EDX) больше размера массива (ECX) - выходим из программы

        end_loop: 
        inc edi                    // EDI = EDI + 1
        jmp begin_loop

        exit_1:
        mov sum_result, eax
    }
    return sum_result;
}

int sum_css(std::vector<int> nums, int lim)
{
    int sum_result = 0;
    for (int i = 0; i < nums.size(); i++)
        if (nums[i] >= lim)
            sum_result += pow(nums[i], 2);
    return sum_result;
}

int main()
{
    setlocale(LC_ALL, "Russian");
    printf("Ассемблер. Лабораторная работа №3. Выполнил студент группы 6101-020302D Абросимов Артём\n========================================================================================\n");
    printf("Вариант 1. Задание: в одномерном массиве A={a[i]} целых чисел вычислить сумму кубов всех\nположительных элементов массива, удовлетворяющих условию: a[i] >=b. \n");

    int lenght, b;
    printf("\nВведите длину массива: "); std::cin >> lenght;
    printf("Введите b: "); std::cin >> b;

    std::string word;
    std::string s;
    std::vector<int> nums(lenght);

    std::cin.ignore(1000, '\n');
    printf("\nПоследовательно введите элементы массива через пробел: "); std::getline(std::cin, s);
    std::stringstream ss(s);

    int j = 0;
    while (std::getline(ss, word, ' '))
    {
        nums[j] = stoi(word);
        j++;
    }

    printf("\nРезультат вычислений на C++: %d", sum_css(nums, b));
    printf("\nРезультат вычислений на asm: %d", sum_asm(nums, b));
}   