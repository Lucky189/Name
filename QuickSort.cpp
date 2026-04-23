#include <iostream>
#include <time.h>

bool IsSorted(int arr[], size_t size)  //upsorted
{
	for (int i = 1; i < size; i++)
	{
		if (arr[i - 1] > arr[i])
			return false;
	}
	return true;
}

void print(int* arr, size_t size)
{
	for (int i = 0; i < size; i++)
	{
		std::cout << arr[i] << ' ';
	}
	std::cout << '\n';
}

void Merge(int arr[], int left, int mid, int right)
{
	int size_left = mid - left + 1;
	int size_right = right - mid;

	int* Left = new int[size_left];
	int* Right = new int[size_right];

	for (int i = 0; i < size_left; i++)
	{
		Left[i] = arr[left + i];
	}
	for (int i = 0; i < size_right; i++)
	{
		Right[i] = arr[mid + i + 1];
	}

	int i = 0, j = 0, k = left;

	while (i < size_left && j < size_right)
	{
		if (Left[i] <= Right[j])
		{
			arr[k] = Left[i];
			i++;
		}
		else
		{
			arr[k] = Right[j];
			j++;
		}
		k++;
	}

	while (j < size_right)
	{
		arr[k] = Right[j];
		j++;
		k++;
	}

	while (i < size_left)
	{
		arr[k] = Left[i];
		i++;
		k++;
	}
	delete[] Left;
	delete[] Right;
}

void MergeSort(int arr[], int left, int right)
{
	if (left < right)
	{
		int mid = left + (right - left) / 2;
		print(arr, right);
		MergeSort(arr, left, mid);
		MergeSort(arr, mid + 1, right);

		Merge(arr, left, mid, right);
	}
}



void QuickSort(int arr[], size_t size)
{
	if (size <= 1)
		return;

	int i = 0, j = size - 1;
	int pivot = arr[size / 2];

	while (i <= j)
	{
		while (arr[i] < pivot) i++;
		while (arr[j] > pivot) j--;

		if (i <= j)
		{
			std::swap(arr[i], arr[j]);
			i++;
			j--;
		}
	}

	// 🔥 правильні рекурсивні виклики
	if (j + 1 > 0)
		QuickSort(arr, j + 1);

	if (size - i > 0)
		QuickSort(arr + i, size - i);
}

int main()
{
	srand(time(NULL));

	const size_t size_ = 10;

	int arr[size_];

	for (int i = 0; i < size_; i++)
	{
		arr[i] = rand() % 100 + 1;
	}

	std::cout << "before sort: \n";
	print(arr, size_);

	std::cout << "call sort function\n";

	//MergeSort(arr, 0, size_ - 1);
	QuickSort(arr, size_);

	std::cout << "\nafter sort: " << '\n';
	print(arr, size_);

	return 0;
}