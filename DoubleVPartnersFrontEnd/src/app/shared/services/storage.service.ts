import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  get = <T>(key?: string) => {
    if (!key) {
      return null
    }
    const jsonValue = sessionStorage.getItem(key)
    if (jsonValue === null) {
      return null
    }
    return JSON.parse(jsonValue) as T
  }

  set = <T>(key: string, value:T) => {
    const jsonValue = JSON.stringify(value)
    sessionStorage.setItem(key, jsonValue)
  }

  remove = (key: string) => sessionStorage.removeItem(key)

  clear = () => sessionStorage.clear()  
}
