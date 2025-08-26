import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { AuthStorage } from '../constans/storage.constant';
import { CurrentUser } from '../models/responses';
import { Breadcrumb } from '../models/components';
import { GlobalStoreState, initialValue } from './global.store.initial';

export const GlobalStore = signalStore(
    { providedIn: 'root'},
    withState<GlobalStoreState>(initialValue),
    withMethods(storeState => {
        return {
            setLogout: () => {
                sessionStorage.removeItem(AuthStorage.TOKEN);
                sessionStorage.removeItem(AuthStorage.CURRENT_USER);
                patchState(storeState, { user: null, isAuthenticated: false });
            },
            setUser: (user: CurrentUser | null) => {
                const isAuthenticated = !!user;
                if (isAuthenticated) {
                    sessionStorage.setItem(AuthStorage.TOKEN, JSON.stringify(user.jwt));
                    sessionStorage.setItem(AuthStorage.CURRENT_USER, JSON.stringify(user));
                }
                patchState(storeState, { user, isAuthenticated  });
            },
            setBreadcrumb: (breadcrumb: Breadcrumb) => {
                sessionStorage.setItem(AuthStorage.BREADCRUMB, JSON.stringify(breadcrumb));
                patchState(storeState, { breadcrumb });
            },
        };
    })
);