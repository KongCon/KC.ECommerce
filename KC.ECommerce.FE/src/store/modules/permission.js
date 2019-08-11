// import {
//     asyncRoutes,
//     constantRoutes
// } from '@/router'

var _router = require('@/router')

/**
 * Use meta.role to determine if the current user has permission
 * @param roles
 * @param route
 */
function hasPermission(menus, route) {
  if (route.name) {
    return menus.includes(route.name)
    // return roles.some(role => route.meta.roles.includes(role))
  } else {
    return true
  }
}

/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param roles
 */
export function filterAsyncRoutes(routes, menus) {
  const res = []

  routes.forEach(route => {
    const tmp = {
      ...route
    }
    if (hasPermission(menus, tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children, menus)
      }
      res.push(tmp)
    }
  })

  return res
}

const state = {
  routes: [],
  addRoutes: []
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.addRoutes = routes
    state.routes = _router.constantRoutes.concat(routes)
  }
}

const actions = {
  generateRoutes({
    commit
  }, menus) {
    return new Promise(resolve => {
      let accessedRoutes
      // if (roles.includes('admin')) {
      //     accessedRoutes = asyncRoutes || []
      // } else {
      //     accessedRoutes = filterAsyncRoutes(asyncRoutes, roles)
      // }
      // accessedRoutes = _router.asyncRoutes || []
      accessedRoutes = filterAsyncRoutes(_router.asyncRoutes, menus)
      // accessedRoutes = asyncRoutes || []
      commit('SET_ROUTES', accessedRoutes)
      resolve(accessedRoutes)
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
