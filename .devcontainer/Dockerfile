FROM mcr.microsoft.com/dotnet/sdk:10.0

# Install common tools (git, procps for ps, unzip, ca-certificates)
USER root
RUN apt-get update \
  && apt-get install -y --no-install-recommends git procps unzip ca-certificates sudo \
  && rm -rf /var/lib/apt/lists/*

# Create a non-root user (vscode) to match common devcontainer conventions
ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=1000
# Create the user only if it doesn't already exist (some base images already have a 'vscode' user/GID)
RUN if ! id -u $USERNAME >/dev/null 2>&1; then \
    groupadd --gid $USER_GID $USERNAME 2>/dev/null || true; \
    useradd -s /bin/bash --uid $USER_UID --gid $USER_GID -m $USERNAME || true; \
    echo "$USERNAME ALL=(ALL) NOPASSWD:ALL" > /etc/sudoers.d/$USERNAME; \
    chmod 0440 /etc/sudoers.d/$USERNAME; \
  fi

WORKDIR /workspace


ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
# Keep the default user as root so local docker run works reliably; VS Code can switch to the
# configured `remoteUser` when opening the devcontainer. (Avoids runtime "no matching entries in passwd" errors.)

# Default shell
SHELL ["/bin/bash", "-lc"]

# Container is ready for use; the devcontainer.json will run restore/build