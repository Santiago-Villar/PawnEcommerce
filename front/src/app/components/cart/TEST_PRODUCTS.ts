import { Product } from "src/app/models/product.model";

export const PRODUCTS: Product[] = [
  {
    id: "1",
    name: "laptop",
    description: "A powerful laptop for all your computing needs.",
    price: 1200,
    color: [
      { id: "1", name: "silver", code: "#C0C0C0" },
      { id: "2", name: "white", code: "#FFFFFF" }
    ],
    brand: { id: "1", name: "Example Brand" },
    category: { id: "1", name: "Electronics" }
  },
  {
    id: "2",
    name: "sneakers",
    description: "Comfortable sneakers for everyday wear.",
    price: 75,
    color: [
      { id: "2", name: "white", code: "#FFFFFF" }
    ],
    brand: { id: "2", name: "Footwear Co." },
    category: { id: "2", name: "Footwear" }
  },
  {
    id: "3",
    name: "smartphone",
    description: "A feature-packed smartphone with advanced technology.",
    price: 800,
    color: [
      { id: "3", name: "black", code: "#000000" }
    ],
    brand: { id: "1", name: "Example Brand" },
    category: { id: "1", name: "Electronics" }
  },
  {
    id: "3",
    name: "smartphone",
    description: "A feature-packed smartphone with advanced technology.",
    price: 800,
    color: [
      { id: "3", name: "black", code: "#000000" }
    ],
    brand: { id: "1", name: "Example Brand" },
    category: { id: "1", name: "Electronics" }
  },
  
  
];

