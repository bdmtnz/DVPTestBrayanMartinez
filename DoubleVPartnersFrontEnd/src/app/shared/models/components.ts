import { MenuItem } from "primeng/api";

export const initialBreadcrumb: Breadcrumb = {
    items: [],
    home: {
        icon: 'pi pi-home',
        routerLink: ['/']
    }
}

export type Breadcrumb = {
    items: MenuItem[]
    home: MenuItem
}