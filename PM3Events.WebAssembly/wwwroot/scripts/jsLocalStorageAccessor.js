class LocalStorageAccessor {
    get = (key) => {
        return window.localStorage.getItem(key);
    }

    set = (key, value) => {
        window.localStorage.setItem(key, value);
    }

    clear = () => {
        window.localStorage.clear();
    }

    remove = (key) => {
        window.localStorage.removeItem(key);
    }
}

const instance = new LocalStorageAccessor();

export function LocalStorageAccessorInstance() {
    return instance;
}