import request from '@/utils/request'

export function fetchList(params) {
    return request({
        url: '/WarningNotice/GetList',
        method: 'get',
        params
    })
}

export function changeHandleStatus(data) {
    return request({
        url: '/WarningNotice/ChangeHandleStatus',
        method: 'post',
        data
    })
}