
Level = {
    PRODUCTION : 0
    STAGE : 1
    DEVELOPMENT : 2
    }

# Define initializations
init = (level = Level.PRODUCTION) ->
    if level > Level.PRODUCTION
        console.log "Initialize script"


# On document ready
$ ->
    init(Level.DEVELOPMENT)


