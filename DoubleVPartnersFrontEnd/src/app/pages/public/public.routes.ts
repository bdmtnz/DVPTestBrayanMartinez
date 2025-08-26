import { Routes } from "@angular/router";

export const publicRoutes: Routes = [
    {
        path: '',
        loadComponent: () => import('./landing/landing').then(m => m.Landing)
    },
    {
        path: 'login',
        loadComponent: () => import('./login/login').then(m => m.Login)
    }
];