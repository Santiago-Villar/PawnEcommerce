export interface Product{
  id: string,
  name: string,
  description: string,
  price: number,
  color: Color[],
  brand: Brand,
  category: Category
}

export interface Color {
  id: string,
  name: string,
  code: string
}

export interface Brand {
  id: string,
  name: string,
}

export interface Category {
  id: string,
  name: string,
}