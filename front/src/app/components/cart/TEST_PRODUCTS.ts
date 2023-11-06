import { Product } from "src/app/models/product.model";

export const PRODUCTS: Product[] = [
  {
    id: "4",
    name: "laptop",
    description: "A powerful laptop for all your computing needs.",
    price: 3200,
    colors: [
      { id: "1", name: "silver", code: "#C0C0C0" },
      { id: "2", name: "white", code: "#FFFFFF" }
    ],
    brand: { id: "1", name: "Example Brand" },
    category: { id: "1", name: "Electronics" }
  },
  {
    id: "5",
    name: "sneakers",
    description: "Comfortable sneakers for everyday wear.",
    price: 4400,
    colors: [
      { id: "2", name: "white", code: "#FFFFFF" }
    ],
    brand: { id: "2", name: "Footwear Co." },
    category: { id: "2", name: "Footwear" }
  },
  {
    id: "6",
    name: "smartphone",
    description: "A feature-packed smartphone with advanced technology.",
    price: 800,
    colors: [
      { id: "3", name: "black", code: "#000000" }
    ],
    brand: { id: "1", name: "Example Brand" },
    category: { id: "1", name: "Electronics" }
  },
  {
    id: "7",
    name: "smartphone",
    description: "A feature-packed smartphone with advanced technology.",
    price: 5600,
    colors: [
      { id: "3", name: "black", code: "#000000" }
    ],
    brand: { id: "1", name: "Example Brand" },
    category: { id: "1", name: "Electronics" }
  },
  
  
];

