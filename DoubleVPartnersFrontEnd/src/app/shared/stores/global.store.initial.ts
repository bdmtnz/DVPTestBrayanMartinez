import { AuthStorage } from "../constans/storage.constant"
import { Breadcrumb, initialBreadcrumb } from "../models/components"
import { CurrentUser } from "../models/responses"

export type GlobalStoreState = {
    isAuthenticated: boolean
    user: CurrentUser | null
    breadcrumb: Breadcrumb
}

export const initialValue: GlobalStoreState = {
    isAuthenticated: !!sessionStorage.getItem(AuthStorage.CURRENT_USER),
    user: !sessionStorage.getItem(AuthStorage.CURRENT_USER) ? null : JSON.parse(sessionStorage.getItem(AuthStorage.CURRENT_USER) || '{}') as CurrentUser,
    breadcrumb: !sessionStorage.getItem(AuthStorage.BREADCRUMB) ? initialBreadcrumb : JSON.parse(sessionStorage.getItem(AuthStorage.BREADCRUMB) || '{}') as Breadcrumb
}