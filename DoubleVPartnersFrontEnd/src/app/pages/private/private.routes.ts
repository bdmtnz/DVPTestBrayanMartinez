import { Routes } from "@angular/router";

export const privateRoutes: Routes = [
    {
        path: 'auth',
        loadComponent: () => import('./layout').then(m => m.Layout),
        children: [
            {
                path: '',
                loadComponent: () => import('./debt/debt').then(m => m.Debt)
            }
        ]
    }
];