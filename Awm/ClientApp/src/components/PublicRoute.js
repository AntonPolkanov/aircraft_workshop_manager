import React from 'react';
import {Route, Redirect} from 'react-router-dom';
import AuthContext from "../auth/AuthContext";

function PublicRoute({component: Component, scopes, ...rest}) {
  return (
    <AuthContext.Consumer>
      { auth => (
        <Route
          {...rest}
          render={props => {
            if (!auth.isAuthenticated())
              return <Component auth={auth} {...props}/>
          }}
        />
      )}
    </AuthContext.Consumer>
  )
}

export default PublicRoute;