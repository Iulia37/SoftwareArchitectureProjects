export type Order = {
    id: number,
    userId: number,
    restaurantId: number,
    menuItems: number[],
    totalPrice: number,
    status: string
}