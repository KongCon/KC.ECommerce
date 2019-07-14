import request from '@/utils/request'

export function login(params) {
    return request({
        url: '/token',
        method: 'get',
        params
    })
}

export function getInfo() {
    return request({
        url: '/user',
        method: 'get'
    })
}

export function logout() {
    return request({
        url: '/user/logout',
        method: 'post'
    })
}