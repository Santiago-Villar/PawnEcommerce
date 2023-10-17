export interface Product{
  id: String,
  name: String,
  description: String,
  price: number,
  color: Color[],
  brand: Brand,
  category: Category
}

export interface Color {
  id: String,
  name: String,
  code: String
}

export interface Brand {
  id: String,
  name: String,
}

export interface Category {
  id: String,
  name: String,
}