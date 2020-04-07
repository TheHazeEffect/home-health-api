import React from 'react'
// import { render } from 'react-dom'
import { Map, Marker, Popup, TileLayer } from 'react-leaflet'

import './Map.css'


export const MapComponent = ({lat,lng,MarkerText}) => {

    // const position = [45.4, -75.7]
    const position = [lat, lng]
    return (

        <Map center={position} zoom={13}>
          <TileLayer
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            attribution="&copy; <a href=&quot;http://osm.org/copyright&quot;>OpenStreetMap</a> contributors"
          />
          <Marker position={position}>
                <Popup>{MarkerText}</Popup>
          </Marker>
        </Map>

        
      )
      
}



