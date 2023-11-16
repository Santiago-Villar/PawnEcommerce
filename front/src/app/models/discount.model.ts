export interface Discount {
  promotionName: string,
  promotionDescription: string,
  paymentMethod: string,
  totalPrice: number,
  promotionDiscount: number,
  paymentMethodDiscount: number,
  finalPrice: number
}