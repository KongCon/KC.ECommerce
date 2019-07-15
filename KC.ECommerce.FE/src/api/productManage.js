import request from '@/utils/request'

export function getList(params) {
    return request({
        url: '/Product/GetList',
        method: 'get',
        params
    })
}