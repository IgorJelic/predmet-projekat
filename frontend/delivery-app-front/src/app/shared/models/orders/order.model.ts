import { OrderedProduct } from './ordered.product.model';

export class Order {
  id: number = 0;
  orderAddress: string = '';
  note: string = '';
  price: number = -1;
  orderedProducts: OrderedProduct[] = [];
  confirmed: boolean = false;
  delivered: boolean = false;
  deliveryTime: Date = new Date();
}
