import { Routes } from '@angular/router';
import { publicRoutes } from './pages/public/public.routes';
import { privateRoutes } from './pages/private/private.routes';

export const routes: Routes = [
    ...publicRoutes,
    ...privateRoutes
];
