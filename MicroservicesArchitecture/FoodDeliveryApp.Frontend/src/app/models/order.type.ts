export type Order = {
    id: number,
    userId: number,
    restaurantId: number,
    totalPrice: number,
    status: string,
    orderDate: Date
}