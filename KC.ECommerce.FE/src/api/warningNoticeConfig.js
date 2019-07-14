import request from '@/utils/request'

export function fetchList(params) {
    return request({
        url: '/WarningNoticeConfig/GetList',
        method: 'get',
        params
    })
}

export function createOrUpdate(data) {
    return request({
        url: '/WarningNoticeConfig/CreateOrUpdate',
        method: 'post',
        data
    })
}

export function deleteData(data) {
    return request({
        url: '/WarningNoticeConfig/Delete',
        method: 'post',
        data
    })
}