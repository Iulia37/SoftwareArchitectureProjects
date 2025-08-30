export type Payment = {
    id: number,
    orderId: number,
    userId: number,
    amount: number,
    method: string,
    address: string,
    createdAt: Date
}