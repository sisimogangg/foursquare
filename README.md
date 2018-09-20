# FoursquareAPI

![](https://img.shields.io/badge/version-1.0-red.svg)
![](https://img.shields.io/badge/net--core-2.1-blue.svg)

# Overview

About a year ago I built quite a simple **Foursquare** [iOS client](https://github.com/sisimogangg/PopularPlaces), where I dealt with one of Foursquare's most complex JSON object.
My approach to the issue back then included having request objects and a separate viewmodel for my view (MVVM). So in essence, I wrote quite a lengthy boiler code.

I have decided to revisit this straight forward App to add more content by integrating it with more public APIs.

The model is obviously becoming more and more complex so I have decided to build a mediator between my App and all public APIs that the app will be consuming.

If you're familiar with [FoursquareAPI](https://developer.foursquare.com/) then you should be aware of how cumbersome APIs can be. More often than not you simply need a subset of the results obtained from the endpoints and combine them with other results from another public API to be able to create a perfect viewmodel for your client. This leads to less time spent on a client.

Currently the mediator service (built in ASP.net Core 2.1) is only handling the **Foursquare** load. I will then in the near future integrate more public APIs.

# Enpoints

## Reuqest

Method: GET

Params: Coordinates (LatLng)

Example:

```http
http://localhost:81/api/places?lat=-29.748428800000003&lng=31.0648832
```

## Response

As of V1, it is just a list of recommended places from foursquare API

```json
[
    {
        "id": "5ba1efca25fb7b002c679bb9",
        "displayType": "venue",
        "venue": {
            "id": "4c0563ce13b99c74bc37aff3",
            "name": "Virgin Active",
            "location": {
                "address": "37 Armstrong Drv",
                "distance": 711,
                "formattedAddress": [
                    "37 Armstrong Drv",
                    "La Lucia",
                    "4051",
                    "iNingizimu Afrika"
                ],
                "coordinates": {
                    "lat": "-29.7546945350829",
                    "lng": "31.0634551876326"
                }
            },
            "venueCategories": [
                "Gym"
            ],
            "stats": {
                "tipCount": 0,
                "usersCount": 0,
                "checkinsCount": 0,
                "visitsCount": 0
            }
        },
      "photo": {
            "prefix": "https://igx.4sqi.net/img/general/",
            "suffix": "/13147188_r0z6oBmNvJlm683rcsnm4NXrToKs4kIXNjiniSYA6Vs.jpg"
        }
    },
    {
        ....
    }
]
```

# Deployment

This will be delivered using Docker. When I built this, my target was Linux containers but the concept is pretty much the same for Windows containers.

## The Dockerfile

This is where the magic happens. I am currently using a concept referred to as Multi-stage building. This is just (in this case) a fancy term for building your code as you build your image. All that matters is that at the end, you will have a production ready image.

```Dockerfile
FROM microsoft/dotnet:2.1.402-sdk AS build # Required for building all .Net Core Applications

WORKDIR /code

COPY . .

RUN dotnet restore

RUN dotnet publish --output /output --configuration Release

FROM microsoft/dotnet:2.1.4-aspnetcore-runtime-alpine # Alpine images are generally fast and light

COPY --from=build /output /app

WORKDIR /app

ENTRYPOINT [ "dotnet", "foursquareApi.dll" ]
```

## Building the image and spinning up a container

### Build image

```bash
docker build -t foursquareAPI:1.0 .
```

### Running image

```bash
docker run -p 81:5001 -d --name foursquareApi foursquareAPI:1.0
```
